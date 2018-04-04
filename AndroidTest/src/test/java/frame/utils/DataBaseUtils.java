package frame.utils;

import frame.Helpers;
import frame.PropertiesResourceManager;

import java.sql.*;

/**
 * Describes functions for working with Data Bases.
 */
public class DataBaseUtils extends Helpers {

	private static PropertiesResourceManager props = new PropertiesResourceManager("database.properties");

	private String driverName;
	private String ip;
	private String port;
	private String baseName;
	private String serverUrl;
	private String userName;
	private String password;
    private String schema;
    private String postfix;
	private Connection connection;
    private DB_TYPE dbType;

    /**
     * Enumeration with database parameters prefix.
     * For example, for DB_TYPE.DB2 and DBpostfix=TEST hostname property name will be "DB2hostnameTEST"
     */
    public enum DB_TYPE {
        MAIN("", "Main database"),
        DB1("DB1", "Database1"),
        DB2("DB2", "Database2"),
        DB3("DB3", "Database3"),
        DB4("DB4", "Database4"),
        DB5("DB5", "Database5");

        private String dbPropertyName, dbDescription;

        DB_TYPE(String dbPropertyName, String dbDescription) {
            this.dbPropertyName = dbPropertyName;
            this.dbDescription = dbDescription;
        }

        public String getDbPropertyName() {
            return dbPropertyName;
        }

        public String getDbDescription() {
            return dbDescription;
        }
    }

	/**
	 * Constructor.
     * Connection will be established with {@link DB_TYPE#MAIN} prefix
	 */
    public DataBaseUtils() {
        this(DB_TYPE.MAIN);
    }

    /**
     * Connection will be stablished from settings with prefix from dbType
     * @param dbType Database type {@link DB_TYPE}
     */
    public DataBaseUtils(DB_TYPE dbType) {
        this.dbType = dbType;
        initializeDB();
        connection = connectToDb(userName, password);
        if (connection != null) {
            doRequest("ALTER SESSION SET CURRENT_SCHEMA=" + schema);
        }
    }

	/**
	 * Variables initialization.
	 */
	private void initializeDB() {
        String dbPropName = dbType.getDbPropertyName();
        postfix = System.getProperty("DBpostfix", "");
        driverName = props.getProperty(String.format("%1$sdriverName%2$s", dbPropName, postfix));
        ip = props.getProperty(String.format("%1$sip%2$s", dbPropName, postfix));
        port = props.getProperty(String.format("%1$sport%2$s", dbPropName, postfix));
        baseName = props.getProperty(String.format("%1$sbaseName%2$s", dbPropName, postfix));
        String serverUrlProp = props.getProperty(String.format("%1$sserverUrl%2$s", dbPropName, postfix));
        serverUrl = String.format(serverUrlProp, ip, port, baseName);
        userName = props.getProperty(String.format("%1$suserName%2$s", dbPropName, postfix));
        password = props.getProperty(String.format("%1$spassword%2$s", dbPropName, postfix));
        schema = props.getProperty(String.format("%1$sschema%2$s", dbPropName, postfix), "");
	}

	/**
	 * Connects to DB
	 * @param user User
	 * @param pass Password
	 * @return Connection connection to database
	 */
	private Connection connectToDb(final String user, final String pass) {
		Connection connect = null;
		try {
			// Load the JDBC driver
			Class.forName(driverName);
			// Create a connection to the database
			connect = DriverManager.getConnection(serverUrl, user, pass);
		} catch (ClassNotFoundException e) {
			logger.error("Unable to connect to the Database " + e.getMessage());
			e.printStackTrace();
		} catch (SQLException e) {
			logger.error("Unable to connect to the Database " + e.getMessage());
			e.printStackTrace();
		}
		logger.info("DB connection established");
		return connect;
	}

	/**
	 * Refreshes connection.
	 */
	private void refreshConnection() {
		try {
			if (connection.isClosed()) {
				logger.info("Connection was closed. Connecting...");
				connection = connectToDb(userName, password);
			}
		} catch (Exception e){
			e.printStackTrace();
		}
	}

    /**
     * Performs request and fail secretCloset on it's fail.
     * @param request Request.
     * @return ResultSet results of request
     */
    public ResultSet doRequest(final String request){
        return doRequest(request, true);
    }

    /**
     * Performs request.
     * @param request Request.
     * @param failOnError If true, secretCloset-case will be failed if error appeared.
     *                    Otherwise - stack trace will be print as warning message.
     * @return ResultSet results of request
     */
    public ResultSet doRequest(final String request, final boolean failOnError){
        ResultSet result = null;
        refreshConnection();
        try {
            Statement statement = connection.createStatement();
            result = statement.executeQuery(request);
        } catch (SQLException e) {
            if (failOnError) {
                logger.fatal(e.getMessage());
            } else {
                logger.warn(e.getMessage());
            }
        }

        if (result == null) {
            if (failOnError) {
                logger.fatal("Database error. Used request: " + request);
            } else {
                logger.warn("Database error. Used request: " + request);
            }
        }
        return result;
    }

    /**
     * Prepare statement and do request
     * @param request SQL request
     * @param failOnError If true, secretCloset-case will be failed if error appeared.
     *                    Otherwise - stack trace will be print as warning message.
     * @param args Arguments for statement
     * @return
     * TODO Experimental feature, not fully tested
     */
    public ResultSet doPreparedStatement(final String request, final boolean failOnError, Object... args) {
        ResultSet result = null;
        refreshConnection();
        try {
            PreparedStatement statement = connection.prepareStatement(request);
            int currIdx = 1;
            for (Object oneObj : args) {
                if (oneObj instanceof Integer) {
                    statement.setInt(currIdx, (Integer)oneObj);
                } else if (oneObj instanceof Boolean) {
                    statement.setInt(currIdx, ((Boolean)oneObj) ? 1 : 0);
                } else if (oneObj == null) {
                    statement.setNull(currIdx, Types.NULL);
                } else {
                    statement.setString(currIdx, (String) oneObj);
                }
                currIdx++;
            }
            result = statement.executeQuery();
        } catch (SQLException e) {
            if (failOnError) {
                logger.fatal(e.getMessage());
            } else {
                logger.warn(e.getMessage());
            }
        }

        if (result == null) {
            if (failOnError) {
                logger.fatal("Database error. Used request: " + request);
            } else {
                logger.warn("Database error. Used request: " + request);
            }
        }
        return result;
    }

	/**
	 * Performs query which may be an INSERT, UPDATE, or DELETE statement.
	 * @param query Query
	 * @return Integer result
	 */
	public int doUpdate(final String query){
		int result = 0;
		refreshConnection();
		try {
			Statement statement = connection.createStatement();
			result = statement.executeUpdate(query);

		} catch (SQLException e) {
			logger.fatal(e.getMessage());
		}
		return result;
	}

	protected String formatLogMsg(final String message) {
        return String.format("Database: '%1$s'.", message);
	}

    /**
     * Close connection. Method should be calling when work with DataBaseUtils object is finished.
     */
    public void closeConnection() {
        try {
            if (!connection.isClosed()) {
                connection.close();
                logger.info("DB connection closed");
            }
        } catch (SQLException ex) {
            ex.printStackTrace();
        }
    }
}

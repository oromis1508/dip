FROM tomcat:8.5-jre8

#Args for git auth
ARG username
ARG password

#Update apps
RUN apt-get -yqq update

#Install apps
RUN apt-get -yqq install default-jdk git maven

#Git settings
RUN git config --global user.name $username
RUN git config --global user.email $username@a1qa.com

#Git clone from CVS
RUN git clone https://$username:$password@qa-gitlab.demohoster.com/automation/docker-test-task.git

#Deploy war
RUN cd docker-test-task && mvn clean package

#Change tomcat server port
RUN sed -i -e 's/8080/8081/g' conf/server.xml

#Move war file to tomcat folder
RUN mv docker-test-task/target/docker-task-1.0.war webapps/

#Start tomcat
#CMD service tomcat start
WORKDIR bin
CMD ["catalina.sh", "run"]


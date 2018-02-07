package demo.test;

public class GameEndException extends Exception {

    public GameEndException(String message) {
        super("Game end with message: " + message);
    }
}

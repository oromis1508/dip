package frame.utils;

public class JamtException extends RuntimeException {

    public JamtException(String msg){
        super(msg+"\n");
    }
}

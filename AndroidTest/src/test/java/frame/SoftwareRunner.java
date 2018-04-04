package frame;

import io.appium.java_client.service.local.AppiumDriverLocalService;
import io.appium.java_client.service.local.AppiumServiceBuilder;

import java.io.File;
import java.util.ArrayList;
import java.util.concurrent.TimeUnit;

import static frame.Helpers.appiumProperties;
import static frame.Helpers.logger;

public class SoftwareRunner {

    private static int appiumPort = Integer.parseInt(appiumProperties.getProperty("appiumPort"));
    private static String appiumIP = appiumProperties.getProperty("appiumIP");
    private static AppiumDriverLocalService appiumProcess;

    private static String genymotionPath = appiumProperties.getProperty("genymotionPath");
    private static String deviceName = appiumProperties.getProperty("deviceName");
    private static int genymotionWait = Integer.parseInt(appiumProperties.getProperty("genWait"));
    private static Process genymotionProcess;

    public static void runAppium() {
        appiumProcess = AppiumDriverLocalService.buildService(new AppiumServiceBuilder().usingPort(appiumPort).withIPAddress(appiumIP));
        appiumProcess.start();
    }

    public static void runGenymotion() {
        String toolname = "gmtool";
        if(getOSType().equals("linux")) {
            toolname = "./" + toolname;
        }

        ArrayList<String> commands = new ArrayList<>();
        commands.add(toolname);
        commands.add("admin");
        commands.add("start");
        commands.add(deviceName);

        ProcessBuilder pb = new ProcessBuilder(commands);
        pb.directory(new File(genymotionPath));

        try {
            genymotionProcess = pb.start();
            genymotionProcess.waitFor(genymotionWait, TimeUnit.SECONDS);
        } catch (Exception e) {
            logger.warn(e.getMessage());
        }
    }

    public static void closeAppium() {
        appiumProcess.stop();
    }

    public static void closeGenymotion() {
        genymotionProcess.destroy();
    }

    private static String getOSType() {
        String osName = System.getProperty("os.name").toLowerCase();
        return osName.contains("win") ? "windows" : "linux";
    }

    public static AppiumDriverLocalService getAppiumProcess() {
        return appiumProcess;
    }
}

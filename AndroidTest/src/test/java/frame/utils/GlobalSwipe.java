package frame.utils;


import frame.Helpers;

public enum GlobalSwipe {

    LEFT{
        @Override
        public void swipe(){
            Helpers.swipe(
                    Helpers.DEVICE_WIDTH - 15
                    , Helpers.DEVICE_HEIGHT / 2
                    , (int) (Helpers.DEVICE_WIDTH * 0.2)
                    , Helpers.DEVICE_HEIGHT / 2);
        }
    },
    RIGHT{
        @Override
        public void swipe(){
            Helpers.swipe(
                    15
                    , Helpers.DEVICE_HEIGHT / 2
                    , Helpers.DEVICE_WIDTH - 15
                    , Helpers.DEVICE_HEIGHT / 2);
        }
    };

    public void swipe(){}
}

package CommandPattern;

public class Stereo {
    public void on() {
        System.out.println("Stereo is on");
    }

    public void setCD() {
        System.out.println("CD is setted");
    }

    public void setVolume(int volume) {
        System.out.println("Volume is " + volume);
    }

    public void off() {
        System.out.println("Stereo is off");
    }
}

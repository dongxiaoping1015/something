package CommandPattern;

import org.omg.CORBA.COMM_FAILURE;

public class RemoteLoader {
    public static void main(String[] args) {
        RemoteControl remoteControl = new RemoteControl();

        Light livingRoomLight = new Light("Living Room");
        Light kitchenLight = new Light("Kitchen");
        GarageDoor garageDoor = new GarageDoor();
        Stereo stereo = new Stereo();

        LightOnCommand livingRoomLightOn =
                new LightOnCommand(livingRoomLight);
        LightOffCommand livingRoomLightOff =
                new LightOffCommand(livingRoomLight);
        LightOnCommand kitchenLightOn =
                new LightOnCommand(kitchenLight);
        LightOffCommand kitchenLightOff =
                new LightOffCommand(kitchenLight);

        GarageDoorOpenCommand garageDoorOpen =
                new GarageDoorOpenCommand(garageDoor);
        GarageDoorCloseCommand garageDoorClose =
                new GarageDoorCloseCommand(garageDoor);

        StereoOnWithCDCommand stereoOnWithCD =
                new StereoOnWithCDCommand(stereo);
        StereoOffCommand stereoOff =
                new StereoOffCommand(stereo);

        remoteControl.setCommand(0, livingRoomLightOn, livingRoomLightOff);
        remoteControl.setCommand(1, kitchenLightOn, kitchenLightOff);
        remoteControl.setCommand(2, garageDoorOpen, garageDoorClose);
        remoteControl.setCommand(3, stereoOnWithCD, stereoOff);

        Command[] partyOn = { livingRoomLightOn, kitchenLightOn, garageDoorOpen, stereoOnWithCD};
        Command[] partyOff = { livingRoomLightOff, kitchenLightOff, garageDoorClose, stereoOff};

        MacroCommand partyOnMacro = new MacroCommand(partyOn);
        MacroCommand partyOffMacro = new MacroCommand(partyOff);

        remoteControl.setCommand(4, partyOnMacro, partyOffMacro);

        System.out.println(remoteControl);


//        remoteControl.onButtonWasPushed(0);
//        remoteControl.offButtonWasPushed(0);
//        remoteControl.onButtonWasPushed(1);
//        remoteControl.offButtonWasPushed(1);
//        remoteControl.onButtonWasPushed(2);
//        remoteControl.offButtonWasPushed(2);
//        remoteControl.onButtonWasPushed(3);
//        remoteControl.offButtonWasPushed(3);
        remoteControl.onButtonWasPushed(4);
        remoteControl.offButtonWasPushed(4);
    }
}

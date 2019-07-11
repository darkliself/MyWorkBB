//§§1682751916141931  Battery Backup & UPS BEGIN Kretinin N. 
USBConnectivity();
CordLengthFt();
RechargeTime();
OutputVoltageCapacity();
PortOrInterface();
BatteryBackupUPSFormFactor();
TypeOfBattery();
LightningProtection();
AdditionalUSBCharging();
AdditionalRemoteBatteryBackupUPS();

void USBConnectivity(){
    if (GetReference("SP-460")!="No"&&GetReference("SP-460")!=""){
        Add("USBConnectivity⸮Provides USB connectivity for maximum flexibility");
    }
}

void CordLengthFt(){
    if (GetReference("SP-21372")!=""){
        Add("CordLengthFt⸮"+GetReference("SP-21372")+"' cord allows you to set up your system at a comfortable distance");
    }
}

void RechargeTime(){
    if (GetReference("SP-22233")!=""){
        Add("RechargeTime⸮"+"Recharge time: "+GetReference("SP-22233")+" hours");
    }
}

void OutputVoltageCapacity(){
    string result="";
    if (GetReference("SP-22337")!=""){
        result="Output voltage of "+GetReference("SP-22337")+" to ensure adequate power supply";
    }
    else if (A[387].HasValue()){
        result="Output voltage of "+A[387].FirstValueOrDefault()+A[387].FirstUnit()+" to ensure adequate power supply";
    }
    if (result!=""){
        Add("OutputVoltageCapacity⸮"+result);
    }
}

void PortOrInterface(){
    if (GetReference("SP-22335")!=""){
        Add("PortOrInterface⸮"+GetReference("SP-22335").Replace(" ", " ##").Replace("x ", ""));
    }
}

void BatteryBackupUPSFormFactor(){
    if (GetReference("SP-22339")!=""){
        Add("BatteryBackupUPSFormFactor⸮"+GetReference("SP-22339").ToLower(true).ToUpperFirstChar()+" form makes it convenient to install ##and use");
    }
}

void TypeOfBattery(){
    string batteryType = GetReference("SP-22342");
    if (batteryType!=""){
        string result="";
        if (batteryType=="Lead Acid Battery"){
            result="Lead acid battery ensures efficient performance";
        }
        else if (batteryType!="Other"&&!batteryType.ToLower().Contains("battery")){
            result=batteryType.ToLower(true).ToUpperFirstChar()+" ensures efficient performance";
        }
        if (result!=""){
            Add("TypeOfBattery⸮"+result);
        }
    }
}

void LightningProtection(){
    if (A[3469].HasValue("%lightning protection%")||checkValue("lightning protection")){
        Add("LightningProtection⸮Lightning protection ensures your investments are safe from potentially irreversible damage");
    }
}

void AdditionalUSBCharging(){
    if (A[374].HasValue("%USB%charging%")){
        Add("AdditionalUSBCharging⸮USB charging port let you charge smartphones or tablets");
    }
}

void AdditionalRemoteBatteryBackupUPS(){
    if (GetReference("SP-22341")=="network management card"||GetReference("SP-22341")=="UPS Management Adapter"){
        Add("AdditionalRemoteBatteryBackupUPS⸮Allows you to reboot your equipment through remote access without any manual help");
    }
}

//§§1682751916141931  Battery Backup & UPS END Kretinin N.
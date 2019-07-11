//§§1682751916141931  "Battery Backup & UPS" BEGIN "Kretinin N." 

// --[FEATURE #1]
// -- Battery backup or UPS device type & Use
void BatteryBackupUPSTypeUse(){
    string type = GetReference("SP-22341");
    if (type!=""){
        string result="";
        if (type=="Battery Backup UPS"||type=="Mini Tower UPS"||type=="UPS"){
            result = type.ToLower(true).ToUpperFirstChar()+" protects electronic equipment and prevents data loss during a power outage";
        }
        else if (type=="Standby UPS"){
            result="Standby UPS protects your equipment in case of power outages or power fluctuations";
        }
        else if (type.ToLower().Contains("replacement battery")||type=="Battery"){
            result = type+" helps restore the runtime of UPS for better performance";
        }
        else if (type=="Extended Battery Module"||type=="UPS Battery Module"){
            result=type.ToLower(true).ToUpperFirstChar()+" for expanded runtime and faster restoration of full backup power";
        }
        else if (type=="Extended Battery Cabinet"||type=="UPS Battery Cabinet"){
            result = type.ToLower(true).ToUpperFirstChar()+" is used for additional run time";
        }
        else if (type=="Display Module"){
            result="Display module allows the user to locally view information";
        }
        else if (type.ToLower().Contains("power module")){
            result=type.ToLower(true).ToUpperFirstChar()+" ensures that systems continue to operate smoothly during power outages";
        }
        else if (type=="Power Conditioner"){
            result="Power conditioner extends the life of the equipment and keeps it in working state in all conditions";
        }
        else if (type=="Step-Down Transformer"){
            result="Step-Down transformer avoids inrush current and saturation eliminating need to oversize transformer";
        }
        else if (type=="Network Management Card"){
            result="Network management card offers remote monitoring and control of UPS";
        }
        else if (type=="UPS Management Adapter"){
            result="UPS management adapter offers remote monitoring and control";
        }
        else if (type=="Intelligence Module"){
            result="Intelligence module is designed to proactively identify and correct problems before downtime occur";
        }
        else if (type=="DC Power Supply"){
            result="DC power supply ensures that you always have a reliable source of electricity";
        }
        else if (type.ToLower().Contains("cable")){
            result = type.ToLower(true).ToUpperFirstChar()+" ensures easy connectivity between devices";
        }
        else if (type.ToLower().Contains("kit")||type.ToLower().Contains("kits")){
            result=type.ToLower(true).ToUpperFirstChar()+" aids for easy installation of equipment";
        }
        else {
            result=type.ToLower(true).ToUpperFirstChar()+" protects electronic equipment";
        }
        if (result!=""){
            Add("BatteryBackupUPSTypeUse⸮"+result);
        }
    }
}
// --[FEATURE #2]
// -- Joule rating
void JouleRating(){
    string joule = GetReference("SP-397");
    if (joule!=""){
        Add("JouleRating⸮Surge energy rating: "+joule+" Joules");
    }
}
// --[FEATURE #3]
// -- Outlet count
void OutletCount(){
    var outlet = GetReference("SP-22340");
    if (outlet!=""){
        string result="Equipped with "+outlet+" outlets for seamless connectivity";
        Add("OutletCount⸮"+result.Replace(" 1 outlets", " 1 outlet"));
    }
}

// --[FEATURE #4]
// -- USB connectivity
void USBConnectivity(){
    if (GetReference("SP-460")!="No"&&GetReference("SP-460")!=""){
        Add("USBConnectivity⸮Provides USB connectivity for maximum flexibility");
    }
}
// --[FEATURE #5]
// -- Cord length (ft.)
void CordLengthFt(){
    if (GetReference("SP-21372")!=""){
        Add("CordLengthFt⸮"+GetReference("SP-21372")+"' cord allows you to set up your system at a comfortable distance");
    }
}

// --[FEATURE #6]
// -- Battery recharge time
void RechargeTime(){
    if (GetReference("SP-22233")!=""){
        Add("RechargeTime⸮"+"Recharge time: "+GetReference("SP-22233")+" hours");
    }
}
// --[FEATURE #7]
// -- Output voltage capacity
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
// --[FEATURE #8]
// -- Port or interface
void PortOrInterface(){
    if (GetReference("SP-22335")!=""){
        Add("PortOrInterface⸮"+GetReference("SP-22335").Replace(" ", " ##").Replace("x ", ""));
    }
}
// --[FEATURE #9]
// -- Battery backup or UPS form factor
void BatteryBackupUPSFormFactor(){
    if (GetReference("SP-22339")!=""){
        Add("BatteryBackupUPSFormFactor⸮"+GetReference("SP-22339").ToLower(true).ToUpperFirstChar()+" form makes it convenient to install ##and use");
    }
}
// --[FEATURE #10]
// -- Type of battery
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
// --[FEATURE #11]
// -- Safety Features (lightning protection)
void LightningProtection(){
    if (A[3469].HasValue("%lightning protection%")||checkValue("lightning protection")){
        Add("LightningProtection⸮Lightning protection ensures your investments are safe from potentially irreversible damage");
    }
}

// --[FEATURE #11]
// Additional-charg USB
void AdditionalUSBCharging(){
    if (A[374].HasValue("%USB%charging%")){
        Add("AdditionalUSBCharging⸮USB charging port let you charge smartphones or tablets");
    }
}

// --[FEATURE #11]
// Additional remote
void AdditionalRemoteBatteryBackupUPS(){
    if (GetReference("SP-22341")=="network management card"||GetReference("SP-22341")=="UPS Management Adapter"){
        Add("AdditionalRemoteBatteryBackupUPS⸮Allows you to reboot your equipment through remote access without any manual help");
    }
}
// --[FEATURE #11]
// Additional Standards

// --[FEATURE #11]
// Additional Dimensions


// --[FEATURE #12]
// -- Warranty

//§§1682751916141931 end of "Battery Backup & UPS"
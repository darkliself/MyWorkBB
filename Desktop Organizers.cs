//§§ "" ""

TypeOfDesktopOrganizerAndItsUse();
// true color material
// Dimensions

// --[FEATURE #1]
// --Type of Desktop Organizer and its use

void TypeOfDesktopOrganizerAndItsUse() {
    var result = "";
    //Card Holders|Pencil Holders|Rotating Organizers|File Organizers|Storage Drawers|Copy Holders|Compartment Storage|Racks|Cabinets|Pen Cups|Accessory Trays|Mobile Device Stand/Holders|Pad Holders|Accessory Holders|Dispensers|Sets|Storage/Document Boxes|Table Tops|Stacking Supports|Letter Holder
    var desktopOrganizerType = R("SP-18586").HasValue() ? R("SP-18586").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18586").HasValue() ? R("cnet_common_SP-18586").Replace("<NULL>", "").Text : "";

    var votexType = A[6284];
    if (!String.IsNullOrEmpty(desktopOrganizerType)) {
        if (desktopOrganizerType.ToLower().Equals("accessory trays")) {
            result = "Provides the extra space we all crave";
        }
        else if (desktopOrganizerType.ToLower().Equals("mobile device stand/holders")) {
            result = "The smart phone holder provides a removable ergonomic system to hold most mobile devices";
        }
        else if (desktopOrganizerType.ToLower().Equals("pad holders")) {
            result = "Pad holders are useful desk accessory";
        }
        else if (desktopOrganizerType.ToLower().Equals("accessory holders")) {
            result = "Accessory holders are useful desk accessory";
        }
        else if (desktopOrganizerType.ToLower().Equals("dispensers")) {
            result = "Dispensers are useful desk accessory";
        }
        else if (desktopOrganizerType.ToLower().Equals("storage/document boxes")) {
            result = "Document box keeps important documents and file folders neatly tucked away";
        }
        else if (desktopOrganizerType.ToLower().Equals("table tops")) {
            result = "Table tops are useful desk accessory";
        }
        else if (desktopOrganizerType.ToLower().Equals("stacking supports")) {
            result = "Stacking supports for trays provide solid support for stacking";
        }
        else if (desktopOrganizerType.ToLower().Equals("letter holder")) {
            result = "Letter holder helps organize cluttered desks";
        }
        else if (desktopOrganizerType.ToLower().Equals("pen cups")) {
            result = "Pen cup keeps pens and pencils organized";
        }
        else if (desktopOrganizerType.ToLower().Equals("cabinets")) {
            result = "Cabinets are useful desk accessory";
        }
        else if (desktopOrganizerType.ToLower().Equals("desk pad")) {
            result = "Desk pad is an excellent desk protector";
        }
        else if (desktopOrganizerType.ToLower().Equals("racks")) {
            result = "Keep messages and other materials organized with this message rack";
        }
        else if (desktopOrganizerType.ToLower().Equals("compartment storage")) {
            result = "Compartment storage is useful desk accessory for organizing your office, craft, or school supplies";
        }
        else if (desktopOrganizerType.ToLower().Equals("copy holders")) {
            result = "Copy holder makes reading at your desk an easier task";
        }
        else if (desktopOrganizerType.ToLower().Equals("pencil holders")) {
            result = "Pencil holder provides plenty of room to store your writing instruments, rulers, and scissors";
        }
        else if (desktopOrganizerType.ToLower().Equals("rotating organizers")) {
            result = "Rotating desk organizer offers easy access to office supplies";
        }
        else if (desktopOrganizerType.ToLower().Equals("file organizers")) {
            result = "File organizer is great for keeping your desktop neat and tidy";
        }
        else if (desktopOrganizerType.ToLower().Equals("Storage Drawers")) {
            result = "Storage Drawer keeps small supplies, writing instruments, and scissors organized";
        }
        else if (desktopOrganizerType.ToLower().Equals("set")) {
            result = "This set is a useful desk accessory";
        }
        else {
            result = $"{Coalesce(desktopOrganizerType.ToLower().ToUpperFirstChar()).Pluralize()} are useful desk accessory";
        }
    }
    else if (votexType.HasValue("paper clip dispenser")) {
        result = "Paper clip dispenser reduces clutter and adds convenience to your workspace";
    }
    
    if (!String.IsNullOrEmpty(result)) {
        Add($"TypeOfDesktopOrganizerAndItsUse⸮{result}");
    }
}
// --[FEATURE #2]
// --True color and Desktop Organizer Material

// --[FEATURE #3]
// --Dimensions in Inches: Height x Width x Depth

// --[FEATURE #4]
// --Feature/Benefit (Capacity as in Letter Size, 7-compartment tray, etc.) Use multiple bullets as necessary

// --[FEATURE #5]
// --

// --[FEATURE #6]
// --

// --[FEATURE #7]
// --

// --[FEATURE #8]
// --

// --[FEATURE #9]
// --

// --[FEATURE #10]
// --

// --[FEATURE #11]
// --

// --[FEATURE #12]
// --Warranty

//§§ "" ""
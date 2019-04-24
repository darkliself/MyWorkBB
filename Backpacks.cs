//§§5884203981142745 "Backpacks" "Alex K." "Serhii.O"
BackpackStyleAndUse();
// color material
BackpackSize();
NumberOfCompartments();

//warranty

// --[FEATURE #1]
// --Backpack style & use

void BackpackStyleAndUse() {
    var result = "";
    if (SKU.ProductId.In("21863890")) {
        result = "Backpack is perfect for carrying all of your supplies";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BackpackStyleAndUse⸮{result}");
    }
}

// --[FEATURE #2]
// --Color family & Material 
//Laptop compatible & compatibility size 


BackpackSize();
void BackpackSize() {
    var result = "";
    var size = R("SP-22643").HasValue() ? R("SP-22643").Replace("<NULL>", "").Text :
		R("cnet_common_SP-22643").HasValue() ? R("cnet_common_SP-22643").Replace("<NULL>", "").Text : "";
    if (SKU.ProductId.In("21863890")) {
        if (!String.IsNullOrEmpty(size)) {
            result = $"Backpack Size: {size}";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BackpackSize⸮{result}");
    }
}


void NumberOfCompartments() {
    var result = "";
    if (SKU.ProductId.In("21863890")) {
         result = $"Features front external pocket to keep you organized";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NumberOfCompartments⸮{result}");
    }
}

// --[FEATURE #7]
// --Weight capacity
void BackpacksWeightCapacity(){
    var result = "";
    
    if(SKU.ProductId.In("21863890")){
        var CarryingCase_Capacity = A[7822]; // Carrying Case - Capacity
        if(CarryingCase_Capacity.HasValue()
        && CarryingCase_Capacity.Units.First().Name.In("liters")){
            result = $"Weight Capacity of {Math.Round(CarryingCase_Capacity.FirstValue() * 2.2)} lbs.";
        }
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"BackpacksWeightCapacity⸮{result}");
    }
}


//5884203981142745 end of "Backpacks" "Alex K." "Serhii.O"§§ 
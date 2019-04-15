
// "Additional Caffeine Free"
void CaffeineFree() {
    var result = "";
    var cafFree = Coalesce(A[6733].HasValue());
    if (cafFree) {
        result = "This drink is caffeine free";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"CafFree⸮{result}");
    }
}

// "Additional Kosher"
void Kosher() {
    var result = "";
    var attrKosher = Coalesce(A[6739]);
    var productLine = !(SKU.ProductLineName is null) ? SKU.ProductLineName.Text.ToLower() : "";
    var modelName = !(SKU.ModelName is null) ? SKU.ModelName.Text.ToLower() : "";
    var description = !(SKU.Description is null) ? SKU.Description.Text.ToLower() : "";
    var isKosher = false;
    if (Coalesce(productLine).In("%kosher%", "%orthodox union kosher%") || Coalesce(modelName).In("%kosher%", "%orthodox union kosher%") || Coalesce(description).In("%kosher%", "%orthodox union kosher%")) {
        isKosher = true;
    }
    if (attrKosher.HasValue("Orthodox Union Kosher")
        || attrKosher.HasValue("kosher")
        || DC.KSP.GetString().In("%kosher%", "%orthodox union kosher%") 
        || DC.MKT.GetString().In("%kosher%", "%orthodox union kosher%")
        || DC.WIB.GetString().In("%kosher%", "%orthodox union kosher%")
        || DC.FEAT.GetString().In("%kosher%", "%orthodox union kosher%")
        || isKosher) {
       result = "The product is kosher";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"Kosher⸮{result}");
    }
}

// "Additional Gluten Free"
void GlutenFree() {
     var result = "";
     var attrGlutenFree = Coalesce(A[6739]);
     var productLine = !(SKU.ProductLineName is null) ? SKU.ProductLineName.Text.ToLower() : "";
    var modelName = !(SKU.ModelName is null) ? SKU.ModelName.Text.ToLower() : "";
    var description = !(SKU.Description is null) ? SKU.Description.Text.ToLower() : "";

     var isGlutenFree = false;
     if (Coalesce(productLine).In("%gluten%free%") || Coalesce(modelName).In("%gluten%free%") || Coalesce(description).In("%gluten%free%")) {
         isGlutenFree = true;
    }
    if (attrGlutenFree.HasValue("gluten-free")
        || DC.KSP.GetString().In("%gluten%free%") 
        || DC.MKT.GetString().In("%gluten%free%")
        || DC.WIB.GetString().In("%gluten%free%")
        || DC.FEAT.GetString().In("%gluten%free%")
        || isGlutenFree) {
        result = "Gluten-free";
    }
     if (!String.IsNullOrEmpty(result)) {
         Add($"GlutenFree⸮{result}");
    }
}


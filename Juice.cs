// 168412514983216257 "Juice" "Alex K." 

JuiceType();
CapacityOz();
// Pack size
Ingredients();
//SugarFree
ArtificialFlavorsOrPreservatives();
//Kosher(); 
//GlutenFree();
Vitamins();
ProductEnergy();
//CaffeineFree();
ContainerType();
CapriSunVaietyPack();

// "Bullet 1" "Juice Type" 
void JuiceType() {
    var juiceType = "";
    var productType = Coalesce(A[6718]);
    var sparkling = Coalesce(A[6720]);
    var type = !(R("SP-22288") is null) || !R("SP-22288").Text.Equals("<NULL>") ? Coalesce(R("SP-22288").Text) : 
    !(R("cnet_common_SP-22288") is null) || !R("cnet_common_SP-22288").Text.Equals("<NULL>") ? Coalesce(R("cnet_common_SP-22288").Text) : Coalesce("");

    if (productType.HasValue("juice") && type.HasValue() && !(type.In("Variety Pack", "Milk", "Cider", "Lemonade"))) {
        if(sparkling.HasValue("yes"))  {
            juiceType = $"Sparkling {type} juice is a refreshing and delicious drink"; 
        }
        else {
             juiceType = $"{type} juice tastes good and is good for you";
        }
    }
    else if (productType.HasValue("juice") && type.HasValue("Variety Pack")) {
        juiceType = "Enjoy a sampling of your favorite juices with this variety pack";
    }
    else if (SKU.Brand.HasValue("CapriSun") && productType.HasValue("water") && type.HasValue("Variety Pack")) {
        juiceType = "Enjoy a sampling of your favorite juices with this variety pack";
    }
    else if (SKU.Brand.HasValue("CapriSun") && productType.HasValue("water") && !type.In("%Milk", "Cider", "Lemonade")) {
        juiceType = $"{type} juice tastes good and is good for you";
    }
    else if (productType.HasValue("soft drink") && !type.In("%Milk", "Variety Pack")) {
        juiceType = $"Quench your thirst with a delicious {type} drink";
    }
    else if (productType.HasValue("instant tea") && !type.In("%Milk", "Variety Pack")) {
        juiceType = $"Sweeten up that boring water with {type}-flavored tea mix";
    }
    else if (productType.HasValue("instant drink") && !type.In("%Milk")) {
        juiceType = $"{type} drink mix transforms ordinary water into a spectacular water experience";
    }
    if (!String.IsNullOrEmpty(juiceType)) {
         Add($"JuiceType⸮{juiceType}");
    }
}

// "Bullet 2" "Capacity (oz.)" 
void CapacityOz() {
    var capacityResult = "";
    var capacity = !(R("SP-21132") is null) || !R("SP-21132").Text.Equals("<NULL>") ? R("SP-21132").Text : 
    !(R("cnet_common_SP-21132") is null) || !R("cnet_common_SP-21132").Text.Equals("<NULL>") ? R("cnet_common_SP-21132").Text : "";
    if (!String.IsNullOrEmpty(capacity)) {
        capacityResult = $"Comes in capacity of {capacity} oz.";
    }
    if (!String.IsNullOrEmpty(capacityResult)) {
        Add($"CapacityOz⸮{capacityResult}");
    }
}

// "Bullet 4" "Ingredients" 
void Ingredients() {
    var result = "";
    var ingredients = Coalesce(A[6726]).WhereNot("vitamin%", "niacin");
    if (ingredients.Flatten().HasValue()) {
        var tmp = ingredients.Select(s => s.Value()).FlattenWithAnd(10, ", ");
        result = $"Made from: {tmp}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"Ingredients⸮{result}");
    }
}

// "Bullet 6" "Artificial flavors or preservatives" 
void ArtificialFlavorsOrPreservatives() {
    var result = "";
    var noPreservatives = Coalesce(A[6739].HasValue("no preservatives"));
    var noArtificialFlavors = Coalesce(A[6739].HasValue("no artificial flavors"));
    if (noPreservatives && noArtificialFlavors) {
       result = "No artificial flavors or preservatives";
    }
    else if (noPreservatives) {
        result = "No artificial flavors";
    }
    else if (noArtificialFlavors) {
        result = "Just real ingredients - no preservatives";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"ArtificialFlavorsOrPreservatives⸮{result}");
    }
}

// "Additional Vitamins" 
//Good source of vitamin C
//https://www.staples.com/Ocean-Spray-100-Orange-Juice-48-count/product_1381180
void Vitamins() {
    var result = "";
    var vitamins = Coalesce(A[6726]).Where("vitamin%", "niacin");
    if (vitamins.Flatten().HasValue()) {
        var tmp = vitamins.Select(s => s.Value()).FlattenWithAnd(10, ", ");
        result = $"Good source of {tmp}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalVitamins⸮{result}");
    }
}
// "Additional Product Energy" 
void ProductEnergy() {
    var result = "";
    var calorieFree = Coalesce(A[6739].HasValue("calorie-free"));
    var energyPerServe = Coalesce(A[11366], A[11367]);
    var energyPerServePer100ml = Coalesce(A[11368], A[11370]);
    if(calorieFree || (energyPerServe.HasValue() && energyPerServe.FirstValue() == 0) 
        || (energyPerServePer100ml.HasValue()) && energyPerServePer100ml.FirstValue()) {
       result = "Free of calories, so you can drink as much as you want without the fear of gaining weight";
    }
    else if (energyPerServe.HasValue() && energyPerServe.FirstValue() > 0) {
        result = $"Energy of drink is {energyPerServe.FirstValue()} {energyPerServe.Units.First().Name}";
    }
    else if (energyPerServePer100ml.HasValue() && energyPerServePer100ml.FirstValue() > 0) {
        result = $"Energy of drink is {energyPerServePer100ml.FirstValue()} {energyPerServePer100ml.Units.First().Name} per 100ml";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalProductEnergy⸮{result}");
    }
}

// "Additional Container Type" 
void ContainerType() {
    var result = "";
    var type = !(R("SP-24477") is null) || !R("SP-24477").Text.Equals("<NULL>") ? R("SP-24477").Text : 
    !(R("cnet_common_SP-24477") is null) || !R("cnet_common_SP-24477").Text.Equals("<NULL>") ? R("cnet_common_SP-24477").Text : "";
    if (!String.IsNullOrEmpty(type)) {
        result = $"Every drink comes in {type}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalContainerType⸮{result}");
    }
}

// "Additional for variety pack (capri sun)" 
void CapriSunVaietyPack() {
    var result = "";
    var brand = Coalesce(SKU.Brand);
    var type = !(R("SP-22288") is null) || !R("SP-22288").Text.Equals("<NULL>") ? Coalesce(R("SP-22288").Text) : 
    !(R("cnet_common_SP-22288") is null) || !R("cnet_common_SP-22288").Text.Equals("<NULL>") ? Coalesce(R("cnet_common_SP-22288").Text) : Coalesce("");
    var capriSun =  Coalesce(A[7338]);
    if (capriSun.HasValue("%- Capri Sun%") && brand.HasValue("CapriSun") && type.HasValue("Variety Pack")) {
        var tmp = capriSun.Where("%Capri %").Select(s => s.Value().ToString().Split(" Sun ").Last()).FlattenWithAnd(20, ",");
        result = $"{capriSun.Where("%Capri %").Count()} different flavors, including {tmp}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalCapriSunVaietyPack⸮{result}");
    }
}

// 168412514983216257 end of "Juice" 

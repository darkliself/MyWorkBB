

//167638614736165755 start of "Post-it&reg; & Sticky Notes" "Alex K." 

TypeOfStickyNotes();
PostItSizeColorCollection();
SheetCountPadsPack();
PopUpOrFlat();
LineType();
DispenserIncluded();
//Greener or Recycled Content (If applicable) 
// Post Consumer Content (If applicable)

// "Bullet 1" "Type of Sticky Notes (including the Theme & Adhesive) & Use" 
void TypeOfStickyNotes() {
    var typeOfStickyNotes = "";
    var stickType = !(R("SP-18402") is null) || !R("SP-18402").Text.Equals("<NULL>") ? R("SP-18402").Text : 
    !(R("cnet_common_SP-18402") is null) || !R("cnet_common_SP-18402").Text.Equals("<NULL>") ? R("cnet_common_SP-18402").Text : "";
    var adhesiveType = A[6031];
    if (!adhesiveType.HasValue("%repositionable%")) {
        typeOfStickyNotes = "Repositionable adhesive won't mark paper and other surfaces";
    }
    else if (adhesiveType.HasValue("%self-adhesive%")) {
        typeOfStickyNotes = "Stick these self-adhesive notes on papers, desks, and anywhere else you need to leave a note";
    }
    else if (adhesiveType.HasValue("full adhesive coated back")) {
        typeOfStickyNotes = "There is full adhesion across the back of each note so the whole pad stays together";
    }
    //Super Sticky|Standard|Full|Dura-Hold
    else if (!String.IsNullOrEmpty(stickType)) {
           typeOfStickyNotes = $"Stick these {stickType} adhesive notes on papers, desks, and anywhere else you need to leave a note"; 
    }

    if (!String.IsNullOrEmpty(typeOfStickyNotes)) {
        Add($"TypeOfStickyNotes⸮{typeOfStickyNotes}");
    }
}

// "Bullet 2" "Post-it Size & Post-it color Collection" 
void PostItSizeColorCollection() {
    var postItSizeColorCollection = "";
    var size  = !(R("SP-18403") is null) || !R("SP-18403").Text.Equals("<NULL>") ? R("SP-18403").Text : 
    !(R("cnet_common_SP-18403") is null) || !R("cnet_common_SP-18403").Text.Equals("<NULL>") ? R("cnet_common_SP-18403").Text : "";
    var width  = !(R("SP-21044") is null) || !R("SP-21044").Text.Equals("<NULL>") ? R("SP-21044").Text : 
    !(R("cnet_common_SP-21044") is null) || !R("cnet_common_SP-21044").Text.Equals("<NULL>") ? R("cnet_common_SP-21044").Text : "";// width in inches
    var length  = !(R("SP-20400") is null) || !R("SP-20400").Text.Equals("<NULL>") ? R("SP-20400").Text : 
    !(R("cnet_common_SP-20400") is null) || !R("cnet_common_SP-20400").Text.Equals("<NULL>") ? R("cnet_common_SP-20400").Text : ""; // length in inches
    var hasWidthAndLength = (!String.IsNullOrEmpty(width) && !String.IsNullOrEmpty(length));
    var colorCollection = R("SP-350290").Text;
    var paperColor = A[6022];
    if (!String.IsNullOrEmpty(size) && size.ToLower().Equals("other")
        && hasWidthAndLength) {
          postItSizeColorCollection = $"{width}\"W x {length}\"L sticky notes";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && paperColor.HasValue("canary yellow")
        && colorCollection.Equals("Assorted")
        && (String.IsNullOrEmpty(size) || !size.ToLower().Equals("other"))
        && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L, Canary Yellow + Assorted Color Notes";
    }
    else if (paperColor.HasValue("%canary yellow%") && hasWidthAndLength) {
        postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the canary yellow attach without staples, paperclips, or tape";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("bora bora")
         && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Bora Bora Collection hold stronger and longer than other notes";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("bali") && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Bali Collection hold stronger and longer than other notes";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("helsinki") && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L Helsinki Collection notes are just the right size to carry around or keep on your desk";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("jaipur") && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L Jaipur Collection notes stand out from the crowd to give you peace of mind";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("miami") && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Miami Collection are a simple tool to keep your everyday organized";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("rio de janeiro") && hasWidthAndLength) {
            postItSizeColorCollection = $"Eye-catching {width}\"W x {length}\"L Rio de Janeiro notes";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("rio de janeiro")) {
            postItSizeColorCollection = $"Grab even the busiest person's attention with the Rio de Janeiro Collection";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("marrakesh") && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Marrakesh Collection keep your messages visible";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("new york") && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L  New York notes include colors inspired by the skyline, stone and steel";
    } 
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("new york")) {
            postItSizeColorCollection = $"New York collection includes colors inspired by the skyline, stone and steel";
    }  
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("marseille") && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Marseille Collection are a simple tool to keep you organized";
    } 
    else if (!String.IsNullOrEmpty(colorCollection) && hasWidthAndLength) {
        postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the {colorCollection} Collection are a simple tool to keep you organized";
    }
    else if (!String.IsNullOrEmpty(colorCollection)) {
        postItSizeColorCollection = $"{colorCollection} Collection notes are a simple tool to keep your everyday organized";
    }

    if (!String.IsNullOrEmpty(postItSizeColorCollection)) {
        Add($"PostItSizeColorCollection⸮{postItSizeColorCollection}");
    }
}

// "Bullet 3" "Sheet Count/Pads per Pack" 
void SheetCountPadsPack() {
    var sheetCountPadsPack = "";
    var pads_x_sheets = Coalesce(A[6110]);
    var TX_UOM =  Coalesce(REQ.GetVariable("TX_UOM"));
    var size = TX_UOM.HasValue("Dozen") ? 12 : TX_UOM.ExtractNumbers().Any() ? TX_UOM.ExtractNumbers().First() : 0;
    var pack = TX_UOM.HasValue() ? TX_UOM.ToString().Split("/").Last() : "";
    var sheets = 0;
    if (pads_x_sheets.HasValue()) {
        sheets = pads_x_sheets.FirstValue().ToString().Split("x").Count() == 2 ? pads_x_sheets.FirstValue().ExtractNumbers().Last() : 0; 
    }
    if (pads_x_sheets.HasValue() && pads_x_sheets.FirstValue().ToString().Split("x").Count() == 2
        && size != 0) {
        sheetCountPadsPack = $"{sheets} notes per pad; {size} pads per pack";
    }
    else if (size != 0) {
        sheetCountPadsPack = $"{size} per {pack}";
    }
    if (!String.IsNullOrEmpty(sheetCountPadsPack)) {
        Add($"SheetCountPadsPack⸮{sheetCountPadsPack}");
    }
}

// "Bullet 4" "Pop up or Flat" 
void PopUpOrFlat() {
   var popUpOrFlat = "";
   var popFlat = !(R("SP-350594") is null) || !R("SP-350594").Text.Equals("<NULL>") ? R("SP-350594").Text : 
    !(R("cnet_common_SP-350594") is null) || !R("cnet_common_SP-350594").Text.Equals("<NULL>") ? R("cnet_common_SP-350594").Text : "";
   if (!String.IsNullOrEmpty(popFlat)) {
       if(popFlat.ToLower().Equals("pop up")) {
           popUpOrFlat = "Notes pop up one after another for one-handed dispensing";
       }
       else if (popFlat.ToLower().Equals("flat")) {
           popUpOrFlat = "Flat pads are perfect for on-the-go use";
       }
   }
   if (!String.IsNullOrEmpty(popUpOrFlat)) {
       Add($"PopUpOrFlat⸮{popUpOrFlat}");
   }
}

// "Bullet 5" "Line type" 
void LineType() {
   var lineType = "";
   var type = !(R("SP-350414") is null) || !R("SP-350414").Text.Equals("<NULL>") ? R("SP-350414").Text : 
    !(R("cnet_common_SP-350414") is null) || !R("cnet_common_SP-350414").Text.Equals("<NULL>") ? R("cnet_common_SP-350414").Text : "";
   if (!String.IsNullOrEmpty(type)) {
       if(type.ToLower().Equals("grid")) {
          lineType = "Grid lined notes are great for techies, teachers and students"; 
       }
       else if (type.ToLower().Equals("lined")) {
          lineType = "Lined Notes provide structure for your thoughts"; 
       }
       else if (type.ToLower().Equals("unlined")) {
          lineType = "Unlined notes provide plenty of room to express yourself"; 
       } 
       else {
           lineType = $"{type} notes provide structure for your thoughts";
       }
   }
   if (!String.IsNullOrEmpty(lineType)) {
       Add($"LineType⸮{lineType}");
   }
}

// "Bullet 6" "Dispenser included (If applicable)" 
void DispenserIncluded() {
   var dispenserIncluded = "";
   var yesOrNo = !(R("SP-350414") is null) || !R("SP-350414").Text.Equals("<NULL>") ? R("SP-350414").Text : 
    !(R("cnet_common_SP-350414") is null) || !R("cnet_common_SP-350414").Text.Equals("<NULL>") ? R("cnet_common_SP-350414").Text : "";
   if (!String.IsNullOrEmpty(yesOrNo) && yesOrNo.ToLower().Equals("yes")) {
     dispenserIncluded = "Dispenser keeps notes organized and easy to find";
   }
   if (!String.IsNullOrEmpty(dispenserIncluded)) {
       Add($"DispenserIncluded⸮{dispenserIncluded}");
   }
}

// "Bullet 7" "Greener or Recycled Content" "for all cat" 

// "Bullet 8" "Post Consumer Content (If applicable)" "for all cat" 

//167638614736165755 end of "Post-it&reg; & Sticky Notes" 


/*
Letter & Desktop Trays
    0E (Filing & Storage)
    MD (AV Furniture)

Specialty Envelopes
    1A (Envelopes & Shipping Boxes)
    0E (Filing & Storage)
    1A (Envelopes & Shipping Boxes)
    CJ (Storage Accessories)

Business Envelopes
    1A (Envelopes & Shipping Boxes)

Report Covers
    0E (Filing & Storage)

Hanging File Folders
    0E (Filing & Storage)

Paper Towels & Dispensers
    2D (Hygiene)
    2B (Cleaning Tools)

Bubble Mailers
    1A (Envelopes & Shipping Boxes)
    1C (Packaging Materials)

File Folders
    0E (Filing & Storage)

Bath Tissue & Dispensers
    2D (Hygiene)

Pocket Folders
    0E (Filing & Storage)
    DJ (Input Accessories)

File Storage
    0E (Filing & Storage)
    4C (Storage Furniture)
    ME (Carrying Cases)

Napkins & Dispensers 
    2D (Hygiene)
    SJ (Tableware)

Clasp & Catalog Envelopes
    1A (Envelopes & Shipping Boxes)
    0E (Filing & Storage)

Post-it & Sticky Notes
    0A (Books & Pads)

Floor Mats
    4D (Decoration)

Accordion Folders
    0E (Filing & Storage)

Classification Folders
    0E (Filing & Storage)

Hanging File Folders
    0E (Filing & Storage)

Binder Accessories
    EI (Printer Consumables)
    0E (Filing & Storage)
    0F (Presentation)
    CJ (Storage Accessories)
    VB (Photo Albums & Archival Storage)
    0A (Books & Pads)
    1C (Packaging Materials)

Binders
    0E (Filing & Storage)
    0F (Presentation)
    ME (Carrying Cases)

---------------------------------------

    0E (Filing & Storage) -- 11306
    MD (AV Furniture) -- OOS
    1A (Envelopes & Shipping Boxes) -- 11306
    CJ (Storage Accessories) -- OOS
    2D (Hygiene) -- OOS
    2B (Cleaning Tools) -- OOS
    1C (Packaging Materials) -- OOS
    DJ (Input Accessories) -- OOS
    4C (Storage Furniture) -- OOS
    ME (Carrying Cases) -- OOS
    SJ (Tableware) -- OOS
    4D (Decoration) -- OOS
    EI (Printer Consumables) -- OOS
    0F (Presentation) -- OOS
    VB (Photo Albums & Archival Storage) -- OOS
    0A (Books & Pads) -- 11306

    IF A[11306].Value IS NOT NULL
THEN A[11306].Value
ELSE IF DC.Ksp.Values.Where("%post-consumer content%").Replace(" post-consumer content","_post-consumer_content").First().Split(" ").Where("%_post-consumer_content%").ExtractDecimals() IS NOT NULL 
THEN DC.Ksp.Values.Where("%post-consumer content%").Replace(" post-consumer content","_post-consumer_content").First().Split(" ").Where("%_post-consumer_content%").ExtractDecimals()
ELSE IF DC.Ksp.Values.Where("%post consumer content%").Replace(" post_consumer content","_post_consumer_content").First().Split(" ").Where("%_post_consumer_content%").ExtractDecimals() IS NOT NULL 
THEN DC.Ksp.Values.Where("%post consumer content%").Replace(" post_consumer content","_post_consumer_content").First().Split(" ").Where("%_post_consumer_content%").ExtractDecimals();

}
*/

var postCont = A[11306];

if (postCont.HasValue()) {
    Add(postCont.FirstValue());
} else {
    var arr = new List<string>() {
        DC.KSP.GetLines().Flatten(" ").ToString(),
        DC.MKT.GetLines().Flatten(" ").ToString(), 
        DC.WIB.GetLines().Flatten(" ").ToString(),
        DC.FEAT.GetLines().Flatten(" ").ToString(), 
        REQ.GetVariable("CNET_SD").ToString(),
        REQ.GetVariable("CNET_HL").ToString() 
    };
    
    if (DC.KSP.HasValue()) {
        arr.Add(DC.KSP.GetString());
    }
    if (DC.MKT.HasValue()) {
        arr.Add(DC.MKT.GetString());
    }
    if (DC.WIB.HasValue()) {
        arr.Add(DC.WIB.GetString());
    }
    if (DC.FEAT.HasValue()) {
        arr.Add(DC.FEAT.GetString());
    }
    if (REQ.GetVariable("CNET_SD").HasValue()) {
        arr.Add(DC.KSP.GetString());
    }
    if (REQ.GetVariable("CNET_HL").HasValue()) {
        arr.Add(DC.KSP.GetString());
    }
    // parcing every element in arr
    var result = new List<int>();
    foreach (var item in arr) {
        var tmp = Coalesce(item);
        if (!(item is null) && item.Contains(" post-consumer content")) {
            foreach (var subItem in item.Replace(" post-consumer content", "_post-consumer_content").Split(" ")) {
                if (subItem.Contains("_post-consumer_content")) {
                    var checkNum = Coalesce(subItem);
                    if(checkNum.ExtractNumbers().Any()) {
                        result.Add(int.Parse(checkNum.ExtractNumbers().First()));
                    }
                }
            }    
        }
    }

    if (result.Count > 0) {
           Add(result.First());
    }
};
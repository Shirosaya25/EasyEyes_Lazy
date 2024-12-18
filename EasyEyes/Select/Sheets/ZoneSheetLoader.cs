using EasyEyes;
using Lumina.Excel.Sheets;
using System;
using System.Linq;
using VFXSelect.Data.Rows;

namespace VFXSelect.Data.Sheets {
    public class ZoneSheetLoader : SheetLoader<XivZone, XivZoneSelected> {
        public override void OnLoad() {
            var sheet = SheetManager.DataManager.GetExcelSheet<TerritoryType>().Where( x => !string.IsNullOrEmpty( x.Name.ExtractText() ) );
            foreach( var item in sheet ) {
                Items.Add( new XivZone( item ) );
            }
        }

        public override bool SelectItem( XivZone item, out XivZoneSelected selectedItem ) {
            selectedItem = null;
            var lgbPath = item.GetLgbPath();
            var result = SheetManager.DataManager.FileExists( lgbPath );
            if( result ) {
                try {
                    var file = SheetManager.DataManager.GetFile<Lumina.Data.Files.LgbFile>( lgbPath );
                    selectedItem = new XivZoneSelected( file, item );
                }
                catch( Exception e ) {
                    Services.Error( e, "Error reading LGB file " + lgbPath );
                    return false;
                }
            }
            return result;
        }
    }
}

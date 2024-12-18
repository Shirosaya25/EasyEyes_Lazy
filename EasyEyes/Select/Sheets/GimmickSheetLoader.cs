using EasyEyes;
using Lumina.Excel.Sheets;
using System;
using System.Collections.Generic;
using System.Linq;
using VFXSelect.Data.Rows;

namespace VFXSelect.Data.Sheets {
    public class GimmickSheetLoader : SheetLoader<XivGimmick, XivGimmickSelected> {
        public override void OnLoad() {
            var territories = SheetManager.DataManager.GetExcelSheet<TerritoryType>().Where( x => !string.IsNullOrEmpty( x.Name.ExtractText() ) ).ToList();
            var suffixToName = new Dictionary<string, string>();
            foreach( var zone in territories ) {
                suffixToName[zone.Name.ToString()] = zone.PlaceName.ValueNullable?.Name.ToString();
            }

            var sheet = SheetManager.DataManager.GetExcelSheet<ActionTimeline>().Where( x => x.Key.ToString().Contains( "gimmick" ) );
            foreach( var item in sheet ) {
                var i = new XivGimmick( item, suffixToName );
                Items.Add( i );
            }
        }

        public override bool SelectItem( XivGimmick item, out XivGimmickSelected selectedItem ) {
            selectedItem = null;
            var tmbPath = item.GetTmbPath();
            var result = SheetManager.DataManager.FileExists( tmbPath );
            if( result ) {
                try {
                    var file = SheetManager.DataManager.GetFile( tmbPath );
                    selectedItem = new XivGimmickSelected( file, item );
                }
                catch( Exception e ) {
                    Services.Error( e, "Error reading TMB file " + tmbPath );
                    return false;
                }
            }
            else {
                Services.Error( tmbPath + " does not exist" );
            }
            return result;
        }
    }
}

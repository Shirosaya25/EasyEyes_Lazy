namespace VFXSelect.Data.Rows {
    public class XivHousing {
        public string Name;
        public string sgbPath;

        public int RowId;
        public ushort Icon;

        // bgcommon/hou/indoor/general/0694/asset/fun_b0_m0694.sgb
        // bgcommon/hou/outdoor/general/0114/asset/gar_b0_m0114.sgb

        public XivHousing( Lumina.Excel.Sheets.HousingYardObject item ) {
            Name = item.Item.Value.Name.ToString();
            RowId = ( int )item.Item.Value.RowId;
            Icon = item.Item.Value.Icon;

            var model = item.ModelKey;
            sgbPath = $"bgcommon/hou/outdoor/general/{model.ToString().PadLeft( 4, '0' )}/asset/gar_b0_m{model.ToString().PadLeft( 4, '0' )}.sgb";
        }

        public XivHousing( Lumina.Excel.Sheets.HousingFurniture item ) {
            Name = item.Item.Value.Name.ToString();
            RowId = ( int )item.Item.Value.RowId;
            Icon = item.Item.Value.Icon;

            var model = item.ModelKey;
            sgbPath = $"bgcommon/hou/indoor/general/{model.ToString().PadLeft( 4, '0' )}/asset/fun_b0_m{model.ToString().PadLeft( 4, '0' )}.sgb";
        }

        public string GetSbgPath() {
            return sgbPath;
        }
    }
}

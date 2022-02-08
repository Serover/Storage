using System.Collections.Generic;

namespace Green_Storage
{

    // Note: Area = Location of the property where storage facillities are - i.e city, building, campus etc.
    public class Area
    {
        private string nameOfArea;

        private string descriptionOfArea;
        private int length;
        private int width;
        private int height;

        // Dictionary where diffrent Areas are stored and listed, not to be mixed with storage locations.
        Dictionary<int, string> dictionaryofAreas = new();


        // List<IStorage> storageList = new();

        public Area(string nameOfArea, string descriptionOfArea, int length, int width, int height)
        {
            this.nameOfArea = nameOfArea;
            this.descriptionOfArea = descriptionOfArea;
            this.length = length;
            this.width = width;
            this.height = height;
        }

        // public void AddStorage(IStorage unit)
        // {
        //     this.storageList.Add(unit);
        // }
    }
}
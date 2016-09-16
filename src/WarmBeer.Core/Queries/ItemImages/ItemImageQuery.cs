namespace WarmBeer.Core.Queries.ItemImages
{
    public class ItemImageQuery
    {

        // look for image in disk 
        // if found return byte array
        // else
        // fecth image from client
        // store image on disk
        // return byte array
        
            // some kind of placeholder if all fails


            // global settings file that can be injected with path to image folder

        public class Parameters
        {
            public Parameters(string itemNumber)
            {
                ItemNumber = itemNumber;
            }

            public string ItemNumber { get; private set; }
        }


        public class Result
        {
            
        }
    }
}

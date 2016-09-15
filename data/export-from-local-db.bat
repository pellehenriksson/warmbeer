bcp [Warm-Beer-Tests].dbo.Items out items.dat -C RAW -T -c -S localhost
bcp [Warm-Beer-Tests].dbo.StoreItems out storeitems.dat -C RAW -T -c -S localhost
bcp [Warm-Beer-Tests].dbo.Stores out stores.dat -C RAW -T -c -S localhost


pause
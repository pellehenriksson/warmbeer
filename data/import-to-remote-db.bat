
bcp [warmbeer-db].dbo.Stores in stores.dat -E -C RAW -T -c -U pelle@warmbeer-sqlserver -S tcp:warmbeer-sqlserver.database.windows.net,1433 -P Rokkahart666

bcp [warmbeer-db].dbo.Items in items.dat -E -C RAW -T -c -U pelle@warmbeer-sqlserver -S tcp:warmbeer-sqlserver.database.windows.net,1433 -P Rokkahart666

bcp [warmbeer-db].dbo.StoreItems in storeitems.dat -E -C RAW -T -c -U pelle@warmbeer-sqlserver -S tcp:warmbeer-sqlserver.database.windows.net,1433 -P Rokkahart666

pause

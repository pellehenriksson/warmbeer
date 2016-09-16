# sqlcmd -S tcp:warmbeer-sqlserver.database.windows.net -d [warmbeer-db] -U pelle@warmbeer-sqlserver -P Rokkahart666  /Q "delete from storeitems;delete from items;delete from stores;"

bcp [warmbeer-db].dbo.Stores in stores.dat -C RAW -E -T -c -U pelle@warmbeer-sqlserver -S tcp:warmbeer-sqlserver.database.windows.net -P Rokkahart666
bcp [warmbeer-db].dbo.Items in items.dat -E -C RAW -T -c -U pelle@warmbeer-sqlserver -S tcp:warmbeer-sqlserver.database.windows.net -P Rokkahart666
bcp [warmbeer-db].dbo.StoreItems in storeitems.dat -E -C RAW -T -c -U pelle@warmbeer-sqlserver -S tcp:warmbeer-sqlserver.database.windows.net -P Rokkahart666

pause

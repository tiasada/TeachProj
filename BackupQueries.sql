BACKUP DATABASE Teach TO DISK = '/var/opt/mssql/data/Teach.bak';

RESTORE DATABASE Teach FROM DISK = '/var/opt/mssql/data/Teach.bak' WITH REPLACE;

DROP DATABASE Teach;
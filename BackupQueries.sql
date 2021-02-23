USE master;
BACKUP DATABASE Teach TO DISK = '/var/opt/mssql/data/Teach.bak';

USE master;
RESTORE DATABASE Teach FROM DISK = '/var/opt/mssql/data/Teach.bak' WITH REPLACE;

DROP DATABASE Teach;
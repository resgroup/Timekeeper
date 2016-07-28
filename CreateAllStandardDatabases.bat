rem field measurement views depend on project and vice versa so must be done using the old vbs scripts. All the other stuff relies on projects or fieldmeasurement in some way so we can't do any of them
rem wind farm also relies on views in controldproduction and vice versa

rem some of the views and stored procs reference kl-sql-001 directly. It needs to be added as a linked server in your local sqlexpress instance or this script will fail.
rem EXEC master.dbo.sp_addlinkedserver @server = N'KL-SQL-001', @srvproduct=N'SQL Server'
rem  /* For security reasons the linked server remote logins password is changed with ######## */
rem EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'KL-SQL-001',@useself=N'True',@locallogin=NULL,@rmtuser=NULL,@rmtpassword=NULL


rem some of the databases will require a kings langley remote user login to exist on the server. Create this first, for example by running the following:
rem /* Add active directory group to server if it does exist */
rem If NOT Exists (select loginname from master.dbo.syslogins where name = 'KingsLangleyRemoteUser')
rem 	CREATE LOGIN KingsLangleyRemoteUser WITH PASSWORD = 'HensLayEggs', CHECK_POLICY = OFF, CHECK_EXPIRATION = OFF 


rem Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Databases\FieldMeasurement. .\sqlexpress FieldMeasurement DropAndCreateEverythingExceptViewsAndPermissions
rem Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Databases\ControlDProduction .\sqlexpress ControlDProduction DropAndCreateEverythingExceptViewsAndPermissions

pushd Databases\FieldMeasurement\
cscript.exe CreateDatabase.vbs FieldMeasurement W7D0632\SQLEXPRESS -v Views\Order.txt
POPD

pushd Databases\ControlDProduction
cscript.exe CreateDatabase.vbs ControlDProduction W7D0632\SQLEXPRESS -v Views\order.txt
popd

Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Databases\WindFarm .\sqlexpress WindFarm DropAndCreateEverythingExceptViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe User\Databases .\sqlexpress User DropAndCreateEverythingExceptViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Project\Databases .\sqlexpress RESProjects DropAndCreateEverythingExceptViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Energy\Gross\Databases .\sqlexpress GrossEnergy DropAndCreateEverythingExceptViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe TurbineModel\Databases .\sqlexpress TurbineModel DropAndCreateEverythingExceptViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe TurbineLayout\Databases .\sqlexpress TurbineLayout DropAndCreateEverythingExceptViewsAndPermissions

Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe User\Databases .\sqlexpress User CreateViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Databases\WindFarm .\sqlexpress WindFarm CreateViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Project\Databases .\sqlexpress RESProjects CreateViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe TurbineModel\Databases .\sqlexpress TurbineModel CreateViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe TurbineLayout\Databases .\sqlexpress TurbineLayout CreateViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Energy\Gross\Databases .\sqlexpress GrossEnergy CreateViewsAndPermissions

# PolicyServer
Mix with PolicyServer.Local and IdentityServer4

## Startup
The best way to test this example easily :

1. Port configuration :
    - MvcClient : http://localhost:5005
    - TestIdentityServer : http://localhost:5000
    - TestPolicyServer : http://localhost:5001
2. Set for each projects the "Project Startup" Mode, NOT the IIS
3. Set multiple projects startup -> set Start for all 3 projects
4. Go to http://localhost:5005, Login with test user (Alice/Bob) and test different URL
5. Should work as expected.... (I hope)

__!! Warning !!__
You must first start the TestIdentityServer project with a custom application arguments: __/seed__ then you can remove it for normal usage.
The __/seed__ parameter creates the 2 user : alice and bob, and correctly migrate the database that identity server needs.
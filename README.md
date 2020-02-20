# PolicyServer
Mix with PolicyServer.Local and IdentityServer4

## Startup
The best way to test this exemple easily :

1. Port configuration :
    - MvcClient : http://localhost:5005
    - TestIdentityServer : http://localhost:5000
    - TestPolicyServer : http://localhost:5001
2. Set for each projects the "Project Startup" Mode, NOT the IIS
3. Set multiple projects startup -> set Start for all 3 projects
4. Go to http://localhost:5005, Login with test user (Alice/Bob) and test different URL
5. Should work as expected.... (I hope)
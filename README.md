## Connection to PostgreSQL

    dotnet user-secrets init
    dotnet user-secrets "ConnectionStrings:Connection" "Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase;Port=5432"

**OR**

Initialize user-secrets
 `dotnet user-secrets init`

Then go to
`%APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json`

Add 

    {
      "ConnectionStrings": {
    	"Connection": "Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase;Port=5432"
      }
    }

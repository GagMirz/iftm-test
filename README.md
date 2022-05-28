**IFTM test task**

**Initial step to do after cloning!!!**

Add your connection string in 

\iftm-test\IFTM.Test.API\WebApplication1\appsettings.json

in connection string field
		  
	"ConnectionStrings": {
		"yourConnectionName": "Server=(yourServer)\\MSSQLLocalDB;Database=yourDbName;Trusted_Connection=True;User Id=yourId;Password=yourPassword;"
	},
		  
**Query to get a books by category**

	{
	  books(category: "Some Category") {
	    author
	    title
	    price
	    categories
	  }
	}

**Mutation to upsert a book:**

	mutation {
	  upsertBook(
    bookDto: {
      author: "Padavana"
      title: "History of Rainbow"
      price: 1000
      categories: ["Music"]
    }
	  ) {
	    author
	    title
	    price
	    categories
	  }
	}

# MHQuestGenerator

A REST api which generates a Monster Hunter World "quest" when given a date. The api is made to simulate daily quests, which are commonly found in online video games. It uses the external [Monster Hunter World API](https://docs.mhw-db.com/) to generate the "monster" in the quest and the "armour set" rewarded upon completing the quest.



## Example:

> POST /api/Quests?date=10-08-22

Output:

```json
[
	{
        id: 1,
        monster: "Kelbi",
        armorSet: "Jagras",
        isComplete: false
	}
]
```



## MSA Requirements

* Create at least two configuration files, and demonstrate the differences between starting the project with one file over another.
  * When using `appsettings.json` it will create a `.log` file which Serilog will save to
  * When using `appsettings.Development.json` it will Serilog will output to console instead
* Demonstrate an understanding of how these middleware via DI (dependency injection) simplifies your code.
  * Dependency injection helps simplify code by making it so that the Controller does not create the objects directly and instead it takes it in as arguments. This allows for decoupling and therefore simplifies our code.
* Demonstrate an understanding of why the middleware libraries made your code easier to test.
  * Swagger provides a template for how calls to the endpoint should be made, which allows for much faster testing compared to manually typing it in Postman.
  * The schema section in Swagger makes it clear what sort of data to expect to be returned from the endpoints.


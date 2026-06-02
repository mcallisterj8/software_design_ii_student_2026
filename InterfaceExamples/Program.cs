Airplane plane = new Airplane { };

// CANNOT instantiate an interface
IFlyable flyingThing = new Airplane { };

flyingThing = new Bird { };


BaldEagle eagle = new BaldEagle { };
eagle.Fly();


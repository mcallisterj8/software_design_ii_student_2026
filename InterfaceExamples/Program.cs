
Airplane planeOne = new Airplane { };

// CANNOT instantiate an interface
// IFlyable theFlyingThing = new IFlyable { };

// CAN use interface as a data type
IFlyable flyingThing = new Airplane { };

flyingThing = new Bird { };
flyingThing = new BaldEagle { };

BaldEagle eagle = new BaldEagle { };
eagle.Fly();

/**************
ADDING TO LISTS
***************/

Bird birdOne = new Bird { };
Bird birdTwo = new Bird { };
Bird birdThree = new BaldEagle { };

BaldEagle baldEagleOne = new BaldEagle { };
BaldEagle baldEagleTwo = new BaldEagle { };


List<Bird> birdList = new List<Bird> {
    birdOne,
    birdTwo,
    birdThree,
    baldEagleOne, // Inherits from Bird
    baldEagleTwo // Inherits from Bird
    // planeOne // Cannot add planeOne to list because not same inheritance
};


Console.WriteLine("=================== Iterating over the flyableList ===================");
List<IFlyable> flyableList = new List<IFlyable> {
    planeOne,

    birdOne,
    birdTwo,
    birdThree,

    baldEagleOne,
    baldEagleTwo
};

foreach (IFlyable elem in flyableList) {
    elem.Fly();
}

Console.WriteLine();








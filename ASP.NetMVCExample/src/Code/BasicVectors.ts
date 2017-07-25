
    export class Vector2D
    {
        //A simple X cord
        public X: number;
        //A simple Y cord
        public Y: number;

        //Constructs a 2D vector X is the horizontal location Y is the Vertical location
        constructor(X: number, Y: number)
        {
            this.X = X;
            this.Y = Y;
        }

        //This funtion Creates a vector with the added vectors from the value being called from and passed and return it for resighning or chaining
        public Add(Vector: Vector2D): Vector2D
        {
            var Return: Vector2D = new Vector2D(this.X, this.Y);
            Return.X += Vector.X;
            Return.Y += Vector.Y;
            return Return;
        }

        //This funtion Creates a vector with the subtracted vectors from the value being called from and passed and return it for resighning or chaining
        public Sub(Vector: Vector2D): Vector2D
        {
            var Return: Vector2D = new Vector2D(this.X, this.Y);
            Return.X -= Vector.X;
            Return.Y -= Vector.Y;
            return Return;
        }

        //This funtion Creates a vector with the scaled vector from the value being used and passed and return it for resighning or chaining
        public Scale(Number: number): Vector2D
        {
            var Return: Vector2D = new Vector2D(this.X, this.Y);
            Return.X *= Number;
            Return.Y *= Number;
            return Return;
        }

        //simply displays the value of the vector as a comma seperated string and a space on the end
        public ToString(): string
        {
            return this.X.toString() + "," + this.Y.toString() + " ";
        }
    }



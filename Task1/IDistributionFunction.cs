namespace Task1
{
    public interface IDistributionFunction
    {
        double F(double x);
        double Left();
        double Right();
}
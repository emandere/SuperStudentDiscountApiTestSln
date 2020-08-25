namespace SuperStudentDiscountApiTests
{
    public class SuperStudentDiscountOracle
    {
        public bool QualifiesForDiscount(SuperStudentDiscountApiTestCase testCase)
        {
            if(testCase.DriverAge < 30 && 
                testCase.DriverGPA >= 3.5 && 
                (testCase.MaritalStatus == "Single" || testCase.MaritalStatus == "Divorced") && 
                testCase.Relationship == "Child" &&
                testCase.StudentStatus.ToLower().Contains("enrolled") && 
                testCase.Violations.Count == 0)
            {
                return true;
            }

            return false;
        }

        public double DiscountAmount(SuperStudentDiscountApiTestCase testCase)
        {
            if(QualifiesForDiscount(testCase) && testCase.DriverGPA >= 3.8)
            {
                return 40.00;
            }
            else if(QualifiesForDiscount(testCase) && testCase.DriverGPA >= 3.5)
            {
                return 20.00;
            }
            else
            {
                return 0;
            }
        }
    }
}

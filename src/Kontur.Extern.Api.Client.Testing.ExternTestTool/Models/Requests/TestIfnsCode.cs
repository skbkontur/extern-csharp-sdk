namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.Models.Requests
{
    public class TestIfnsCode
    {
        public static TestIfnsCode Code_0007 = new("0007");
        public static TestIfnsCode Code_0008 = new("0008");
        public static TestIfnsCode Code_0084 = new("0084");
        public static TestIfnsCode Code_0085 = new("0085");
        public static TestIfnsCode Code_0087 = new("0087");
        public static TestIfnsCode Code_0088 = new("0088");
        public static TestIfnsCode Code_0093 = new("0093");
        public static TestIfnsCode Code_0094 = new("0094");
        public static TestIfnsCode Code_0096 = new("0096");
        public static TestIfnsCode Code_9979 = new("9979");
        public static TestIfnsCode Code_7702 = new("7702");
        
        private readonly string value;

        private TestIfnsCode(string value) => this.value = value;

        public override string ToString() => value;
    }
}
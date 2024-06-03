namespace PlcComm.Common
{
    public class TypeConversions
    {
        /// <summary>
        /// Converts an integer into a binary string.
        /// </summary>
        /// <param name="value">Input integer value</param>
        /// <param name="length">Length of the desired binary string. The string is Left-Padded with 0's if the input integer is not large 
        /// enough to reach the desired binary string length</param>
        /// <returns>List of boolean values that is the equivalent of the binary string. The right-most value in the binary string is the
        /// first position in the list of booleans</returns>
        public static List<bool> IntToBinaryListOfBoolsConverter(int value, int length)
        {
            string binaryString = Convert.ToString(value, 2);
            binaryString = binaryString.PadLeft(length, '0');
            List<bool> listOfBools = new List<bool>();
            foreach (char c in binaryString)
            {
                bool x = Convert.ToBoolean(Convert.ToInt32(c.ToString()));
                listOfBools.Add(x);
            }
            listOfBools.Reverse();
            return listOfBools;
        }
    }
}

[System.Serializable]

public class QnAHolder
{
    //String that gets used in order to create the different questions created in Questionmanager
    public string questions;

    //Array of string of possible answer to the question above
    public string[] answers;

    //Int of the correct answer based on the array created above
    public int correctAnswer;
}

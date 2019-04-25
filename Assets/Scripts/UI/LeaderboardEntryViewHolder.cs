using TMPro;

public class LeaderboardEntryViewHolder : ViewHolder<LeaderboardEntry>
{
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dateText;

    public override void BindData(LeaderboardEntry data)
    {
        if (positionText != null)
        {
            positionText.text = data.Position.ToString();
        }

        if (nameText != null)
        {
            nameText.text = data.Name;
        }

        if (scoreText != null)
        {
            scoreText.text = data.Score.ToString();
        }

        if (timeText != null)
        {
            timeText.text = data.Time.ToString();
        }
        
        if (dateText != null)
        {
            dateText.text = data.Date;
        }
    }
}
namespace DB.Entity;

public class ExperimentEntity
{
    public int ExperimentId { get; set; }
    public List<CardEntity> Deck { get; set; }
}
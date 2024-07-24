<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$cuid = $_POST["cuid"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error)
}	
	echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

$sql = "SELECT 	* 
		FROM 	questdata 
		WHERE 	quest_id not IN (SELECT quest_id
								 FROM player_questdata
								 WHERE character_uid = $cuid and iscleared = 'Y')";
							   
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) > 0)
{
	$quests = array();
	while ($row = mysqli_fetch_assoc($result))
	{
		$quests[] = array(
			'quest_name' => $row['quest_name'],
			'quest_id' => $row['quest_id'],
			'giver_id' => $row['giver_id'],
			'target_type' => $row['target_type'],
			'target_id' => $row['target_id'],
			'quest_goalline' => $row['quest_goalline'],
			'gold_reward' => $row['gold_reward'],
			'exp_reward' => $row['exp_reward']);
	}
	$quests_json_string = json_encode($quests);
	echo $quests_json_string;
}	
else
{
	return;
}
?>
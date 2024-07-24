<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$cuid = $_POST["cuid"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error)
{	
	echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

$sql = "SELECT 	quest_id, current, isdone 
		FROM 	player_questdata
		WHERE 	character_uid = $cuid and iscleared = 'N'";
		
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) > 0)
{
	$row = mysqli_fetch_assoc($result);
	echo $row['quest_id'].",".$row['current'].",".$row['isdone'];
}
else
{
	echo 0;
}

return;
?>
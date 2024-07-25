<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$cuid = $_POST["cuid"];
$questid = $_POST["questid"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error)
{	
	echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

$sql = "UPDATE 	player_questdata
		SET 	iscleared = 'Y'
		WHERE  	character_uid = $cuid AND quest_id = $questid";
		
$result = mysqli_query($conn, $sql);

if ($result)
{
	echo 1
}
else
{
	echo 0;
}

return;
?>
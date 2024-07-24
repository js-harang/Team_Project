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

$sql = "select 	quest_id 
		from 	player_questdata
		where 	character_uid = $cuid and iscleared = 'Y'";
		
$result = mysqli_query($conn, $sql);

if ($result)
{
	$row = mysqli_fetch_assoc($result);
	echo $row['quest_id'];
}
else
{
	echo 0;
}

return;
?>
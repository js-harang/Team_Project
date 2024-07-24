<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error)
}	
	echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

$sql = "select * from questdata;";
$result = mysqli_query($conn, $sql);
?>
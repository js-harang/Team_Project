<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$uid = $_POST["uid"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

$sql = "SELECT	slot, name, lv, class
		from	character_list
		where	uid = $uid
        order by slot";

$result = mysqli_query($conn, $sql);

while ($row = mysqli_fetch_array($result)) {
    echo $row['slot'] . " " . $row['name'] . " " . $row['lv'] . " " . $row['class'] . "<br>";
}

mysqli_close($conn);
?>
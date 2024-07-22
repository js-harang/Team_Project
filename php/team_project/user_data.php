<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$userid = $_POST["userid"];
$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

$sql = "SELECT  uid
        from    user
        where   userid = '$userid'";
$result = mysqli_query($conn, $sql);
$row = mysqli_fetch_assoc($result);
echo $row['uid'];

return;
?>
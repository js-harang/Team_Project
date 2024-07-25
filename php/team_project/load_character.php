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

if (mysqli_num_rows($result) > 0) {
    while ($row = mysqli_fetch_assoc($result)) {
        $datas[] = array(
            'slot' => $row['slot'],
            'name' => $row['name'],
            'lv' => $row['lv'],
            'class' => $row['class']
        );
    }

    $datas_json_string = json_encode($datas, JSON_UNESCAPED_UNICODE);
    echo $datas_json_string;
}

mysqli_close($conn);
?>
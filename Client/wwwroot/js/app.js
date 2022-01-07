
function show_alert(message)
{
    alert(message);
}

function play_audio(audio_player_id)
{
    let audio_player = document.getElementById(audio_player_id);
    audio_player.load();
    audio_player.play();
}

function pause_audio(audio_player_id)
{
    let audio_player = document.getElementById(audio_player_id);
    audio_player.pause();
}
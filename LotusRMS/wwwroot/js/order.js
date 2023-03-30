"use strict";

const client = new signalR.HubConnectionBuilder()
    .withUrl("/orderHub")
    .build();

client.on("OrderComplete", newCall => {
    console.log(newCall);
    if ('speechSynthesis' in window) {
        // Speech Synthesis supported 🎉
        var msg = new SpeechSynthesisUtterance();
        msg.text = "Order for "+newCall[1]+" complete for table "+newCall[0];
        window.speechSynthesis.speak(msg);

    } else {
        // Speech Synthesis Not Supported 😣
        alert("Sorry, your browser doesn't support text to speech!");
    }

    
});

document.addEventListener("DOMContentLoaded", () => {
    client.start();
    speechSynthesis.getVoices().forEach(function (voice) {
        console.log(voice.name, voice.default ? voice.default : '');
    });
    var msg = new SpeechSynthesisUtterance();
    var voices = window.speechSynthesis.getVoices();
    console.log(voices);
    msg.voice = voices[1];
    msg.volume = 1; // From 0 to 1
    msg.rate = 1; // From 0.1 to 10
    msg.pitch = 2; // From 0 to 2
    msg.text = "welcome to the RMS  ";
    msg.lang = 'en';
    speechSynthesis.speak(msg);

});
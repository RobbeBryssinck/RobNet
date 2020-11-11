import React, { useState } from "react";

function AddBotForm({ addBot }) {
  const [ip, setIp] = useState("");
  const [platform, setPlatform] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(ip);
    console.log(platform);
    if (!ip || !platform) return;

    const bot = {
      ip: ip,
      platform: platform,
      status: "Waiting",
      botnetId: 1,
    };
    console.log("Calling...");
    addBot(bot);

    setIp("");
    setPlatform("");
  };

  return (
    <form onSubmit={handleSubmit} style={{ margin: "20px 0px" }}>
      <input
        type="text"
        placeholder="IP Address"
        value={ip}
        onChange={(e) => setIp(e.target.value)}
      ></input>
      <select value={platform} onChange={(e) => setPlatform(e.target.value)}>
        <option value="Linux">Linux</option>
        <option value="Windows">Windows</option>
      </select>
      <button className="grey-button">Add bot to botnet</button>
    </form>
  );
}

export default AddBotForm;

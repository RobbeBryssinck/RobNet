import React, { useState } from "react";

function AddBotForm({ addBot, botnetId }) {
  const [ip, setIp] = useState("");
  const [platform, setPlatform] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!ip || !platform) return;

    const bot = {
      ip: ip,
      platform: platform,
      botnetId: botnetId,
    };
    addBot(bot);

    setIp("");
    setPlatform("");
  };

  return (
    <form onSubmit={handleSubmit} style={{ margin: "20px 0px" }}>
      <label>
        IP:
        <input
          type="text"
          placeholder="IP Address"
          value={ip}
          onChange={(e) => setIp(e.target.value)}
        ></input>
      </label>
      <label>
        Platform:
        <select value={platform} onChange={(e) => setPlatform(e.target.value)}>
          <option value="Linux">Linux</option>
          <option value="Windows">Windows</option>
        </select>
      </label>
      <button className="grey-button">Add bot to botnet</button>
    </form>
  );
}

export default AddBotForm;

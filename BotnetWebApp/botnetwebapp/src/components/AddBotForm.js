import React, { useState } from "react";

function AddBotForm({ addBot }) {
  const [bot, setBot] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!bot) return;
    addBot(bot);
    setBot("");
  };

  return (
    <form onSubmit={handleSubmit}>
      <input
        type="text"
        placeholder="IP Address"
        value={bot}
        onChange={(e) => setBot(e.target.value)}
      ></input>
      <button className="grey-button">Add bot to botnet</button>
    </form>
  );
}

export default AddBotForm;

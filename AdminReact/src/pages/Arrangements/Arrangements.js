import React, { useState, useEffect } from "react";
import Header from "../../components/Header";
import ArrangementsList from "./components/ArrangementsList";
import { getArrangements } from "../../services/ArrangementsService";

function Arrangements() {
  const [arrangements, setArrangements] = useState([]);

  useEffect(() => {
    getArrangements().then((_arrangements) => setArrangements(_arrangements));
  });

  return (
    <>
      <Header />
      <div className="container py-5">
        <ArrangementsList arrangements={arrangements} />
      </div>
    </>
  );
}

export default Arrangements;

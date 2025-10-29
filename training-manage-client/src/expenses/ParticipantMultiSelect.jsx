import React, { useState, useMemo } from "react";

export default function ParticipantMultiSelect({
  persons,        // pole { id, name, identificationNumber }
  selectedIds,    // pole vybraných ID
  onChange        // fn(nextSelectedIds)
}) {
  const [filter, setFilter] = useState("");

  // Filtrovaný seznam podle jména nebo čísla
  const filtered = useMemo(() => {
    const term = filter.trim().toLowerCase();
    if (!term) return persons;
    return persons.filter(p =>
      p.name.toLowerCase().includes(term) ||
      p.identificationNumber.includes(term)
    );
  }, [filter, persons]);

  // Vrátí true, když jsou všechny filtrované osoby vybrané
  const allFilteredSelected = filtered.length > 0 &&
    filtered.every(p => selectedIds.includes(p.id));

  // Vyber / odznač všechny aktuálně filtrované
  const toggleAll = () => {
    if (allFilteredSelected) {
      // Odstraníme filtrované z výběru
      onChange(selectedIds.filter(id => !filtered.some(p => p.id === id)));
    } else {
      // Přidáme všechny filtrované
      const newIds = [
        ...new Set([
          ...selectedIds,
          ...filtered.map(p => p.id)
        ])
      ];
      onChange(newIds);
    }
  };

  // Přepne jeden ID
  const toggleOne = id => {
    if (selectedIds.includes(id)) {
      onChange(selectedIds.filter(x => x !== id));
    } else {
      onChange([...selectedIds, id]);
    }
  };

  {filtered.map(p => (
  <div
    key={p.id}
    className="d-flex align-items-center px-2 py-1 participant-row"
    style={{ cursor: "pointer" }}
    onClick={() => toggleOne(p.id)}
  >
    <input
      type="checkbox"
      className="form-check-input me-2"
      checked={selectedIds.includes(p.id)}
      onChange={() => toggleOne(p.id)}
    />
    <label className="form-check-label">
      {p.name} ({p.identificationNumber})
    </label>
  </div>
))}

  return (
    <div>
      {/* Hledání + tlačítko Vybrat vše */}
      <div className="d-flex mb-2">
        <input
          type="text"
          className="form-control me-2"
          placeholder="Hledej jméno nebo číslo..."
          value={filter}
          onChange={e => setFilter(e.target.value)}
        />
        <button
          type="button"
          className="btn btn-outline-secondary"
          onClick={toggleAll}
        >
          {allFilteredSelected ? "Odznačit vše" : "Vybrat vše"}
        </button>
      </div>

      {/* Scrollovatelný seznam */}
      <div
        style={{
          maxHeight: 200,
          overflowY: "auto",
          border: "1px solid #dee2e6",
          borderRadius: 4
        }}
      >
        {filtered.map(p => (
          <div
            key={p.id}
            className="d-flex align-items-center px-2 py-1"
            style={{ cursor: "pointer" }}
            onClick={() => toggleOne(p.id)}
          >
            <input
              type="checkbox"
              className="form-check-input me-2"
              checked={selectedIds.includes(p.id)}
              onChange={() => toggleOne(p.id)}
            />
            <span>{p.name} ({p.identificationNumber})</span>
          </div>
        ))}

        {filtered.length === 0 && (
          <div className="text-muted p-2">Žádní účastníci</div>
        )}
      </div>
    </div>
  );
}

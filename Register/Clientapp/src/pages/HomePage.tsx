import React, { useState, useRef } from 'react';
import { Tabs, Tab, Box, Typography } from '@mui/material';
import PersonForm from '../components/PersonForm';
import PersonList, { PersonListHandles } from '../components/PersonList';
import PersonFormV2 from '../components/V2/PersonFormV2';
import PersonListV2, { PersonListV2Handles } from '../components/V2/PersonListV2';
import { Person } from '../types/Person';
import { PersonV2 } from '../types/V2/PersonV2';

const HomePage: React.FC = () => {
  const [tab, setTab] = useState(0);

  const [selectedPersonV1, setSelectedPersonV1] = useState<Person | undefined>();
  const [selectedPersonV2, setSelectedPersonV2] = useState<PersonV2 | undefined>();

  const personListV1Ref = useRef<PersonListHandles>(null);
  const personListV2Ref = useRef<PersonListV2Handles>(null);

  const handleChange = (_event: React.SyntheticEvent, newValue: number) => {
    setTab(newValue);
  };

  const handleSavedV1 = () => {
    setSelectedPersonV1(undefined);
    personListV1Ref.current?.loadPersons();
  };

  const handleSavedV2 = () => {
    setSelectedPersonV2(undefined);
    personListV2Ref.current?.loadPersonsV2();
  };

  return (
    <Box p={2}>
      <Typography variant="h4" gutterBottom>
        Gestão de Pessoas
      </Typography>
      <Tabs value={tab} onChange={handleChange}>
        <Tab label="Versão 1" />
        <Tab label="Versão 2" />
      </Tabs>
      {tab === 0 && (
        <Box mt={3}>
          <PersonForm
            selectedPerson={selectedPersonV1}
            onSaved={handleSavedV1}
          />
          <PersonList
            ref={personListV1Ref}
            onEdit={(person) => setSelectedPersonV1(person)}
          />
        </Box>
      )}
      {tab === 1 && (
        <Box mt={3}>
          <PersonFormV2
            selectedPerson={selectedPersonV2}
            onSaved={handleSavedV2}
          />
          <PersonListV2
            ref={personListV2Ref}
            onEdit={(person) => setSelectedPersonV2(person)}
          />
        </Box>
      )}
    </Box>
  );
};

export default HomePage;

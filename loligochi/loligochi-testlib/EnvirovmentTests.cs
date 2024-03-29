﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using loligochi_classlib;
using System;
using System.IO;
using System.Text.Json;

namespace loligochi_testlib
{
    [TestClass]
    public class EnvirovmentTests
    {
        [TestMethod]
        public void SerializeEntity_CreatesFileWithProperContent()
        {
            var entity = new Entity("deadImage", "normalImage", "hungryImage",
                "thirstyImage", "angryImage", "sickImage",
                "deadVoice", "normalVoice", "sickVoice",
                "hungryVoice", "thirstyVoice", "angryVoice",
                "Normal", "TestEntity", "BasedOnEntity", 1,
                5, 100, 50, 50, false, 0, 100, 50, DateTime.Now);

            var envirovment = new Envirovment();
            var fileName = "TestEntity";

            Directory.CreateDirectory("src/save");

            var result = Envirovment.SerializeEntity(entity, fileName);
            var filePath = Path.Combine("src/save", fileName + ".json");

            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(filePath));
            var jsonString = File.ReadAllText(filePath);
            var deserializedEntity = JsonSerializer.Deserialize<Entity>(jsonString);
            Assert.IsNotNull(deserializedEntity);
            Assert.AreEqual("TestEntity", deserializedEntity.Name);

            File.Delete(filePath);
        }

        [TestMethod]
        public void GetAvaibleSaves_ReturnsListOfSaveFiles()
        {
            Directory.CreateDirectory("src/save");
            var filePath = Path.Combine("src/save", "TestSave.json");
            File.WriteAllText(filePath, "{}"); 

            var saves = Envirovment.GetAvaibleSaves();

            Assert.IsTrue(saves.Contains(filePath));

            File.Delete(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(FileMissingException))]
        public void DeserializeEntity_ThrowsFileMissingExceptionIfFileNotExist()
        {
            var filePath = "non_existing_path.json";

            Envirovment.DeserializeEntity(filePath);

        }

        [TestMethod]
        public void DeserializeEntity_ReturnsEntityWithProperContent()
        {
            var entity = new Entity("deadImage", "normalImage", "hungryImage",
                "thirstyImage", "angryImage", "sickImage",
                "deadVoice", "normalVoice", "sickVoice",
                "hungryVoice", "thirstyVoice", "angryVoice",
                "Normal", "TestEntity", "BasedOnEntity", 1,
                5, 100, 50, 50, false, 0, 100, 50, DateTime.Now);

            var jsonString = JsonSerializer.Serialize(entity);
            var filePath = Path.Combine("src/save", "TestEntity.json");
            Directory.CreateDirectory("src/save");
            File.WriteAllText(filePath, jsonString);

            var deserializedEntity = Envirovment.DeserializeEntity(filePath);

            Assert.IsNotNull(deserializedEntity);
            Assert.AreEqual("TestEntity", deserializedEntity.Name);

            File.Delete(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(SerializationErrorException))]
        public void DeserializeEntity_ThrowsSerializationErrorExceptionOnInvalidJson()
        {
            var invalidJsonString = "invalid_json";
            var filePath = Path.Combine("src/save", "TestEntity.json");
            Directory.CreateDirectory("src/save");
            File.WriteAllText(filePath, invalidJsonString);

            Envirovment.DeserializeEntity(filePath);

            File.Delete(filePath);
        }
    }
}
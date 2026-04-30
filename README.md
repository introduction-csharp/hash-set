# hash-sets


A [hash set](https://en.wikipedia.org/wiki/Set_(abstract_data_type)) is an abstract data type with an array/list to contain the data. It acts like a mathematical set in that it allows no duplicates. It is often that the underlying data structure is of fixed size. Items are stored in the structure at particular locations, but this location is calculated from their value. Thus the _index_ into the list is a _hash_ of their value, derived with a specific algorithm (possibly _seeded_ with some constant extra value) and then constrained, via mod or %, to be within that fixed size.  

The hashing algorithm is designed to allow for uniform coverage of all possible location indices. 

## Collisions 

Given there are fewer slots in the structure than there are possible values to store, there will be occasions where two items will have identicial keys, these are called _collisions_. 

What to do in these cases? There are two approaches: _linear probing_ and _chaining_. 

(Well, strictly there's a third, to resize the array, but that's expensive and needs *all* keys to be recalculated.) 

### Linear Probing 

This involves iteratively moving down the array and putting the data in the next available free slot. 

### Chaining 

This involves pointing to a second data store (possibly another instance of the HashSet) and then storing all those who would share that slot in this new collection. 

## Further Reading 

- Bond 1, pp285--298. 
- [Baeldung.com](https://www.baeldung.com/cs/hash-tables) 
- [wikipedia](https://en.wikipedia.org/wiki/Hash_table)
- [wikibooks](https://en.wikibooks.org/wiki/Data_Structures/Hash_Tables)

## Tasks

See the [shared document](https://docs.google.com/document/d/1lXkSRJYWdgIysSzLaU5dLR_b3a0rKdVCAc9pabMqvec/edit?usp=sharing) for some things to do. 

*NB* there is a built-in `HashSet` class, you should ignore this for now. 

There is an IHashSet.cs file in this repo which you could choose to use as the basis for the work outlined in the document. 
